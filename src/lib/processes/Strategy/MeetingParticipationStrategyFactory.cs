using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Domain;
using managers.Resource;
using processes.Engine;

namespace processes.Strategy
{
    public class MeetingParticipationStrategyFactory: BaseStrategyFactory
    {
        private const string _meetingParamName = "meeting";
        private const string _userIdParamName = "userId";
        private const string _registerIdParamName = "register";
        private readonly IMeetingResourceManager _manager;

        private Action<MeetingDto, long, bool> _consolidateHandle = (MeetingDto m, long userId, bool deregister) => 
        {
            var mapping = m.Participants.FirstOrDefault(p => p.UserId == userId); 
            if(mapping == null)
            {
                m.Participants.Add(new MeetingUserMappingDto(m.Id, userId));
            }
        };

        public MeetingParticipationStrategyFactory(
            IMeetingResourceManager resourceManager
        ): base(null) => this._manager = resourceManager;

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var meeting = param[_meetingParamName].Cast<MeetingDto>();
                var userId = param[_userIdParamName].Cast<long>();
                var registering = param[_registerIdParamName].Cast<bool>();

                // Check if mapping already exists.
                if(registering && meeting.Participants.Any(p => p.UserId.Equals(userId)))
                {
                    throw new InvalidOperationException("User already registerd for meeting");
                }

                if(registering)
                {
                    await this._manager.AddMeetingUserMapping(meeting.Id, userId);
                }
                else
                {
                    await this._manager.RemoveMeetingUserMapping(meeting.Id, userId);

                }

                return null;
            };
        }
    }
}