//using DTO.Tracking;
//using managers.Resource;
//using System.Threading.Tasks;
//using System;

//namespace processes.Adapter
//{
    //public class MailSendTrackingAdapter: ITrackingAdapter
    //{
        //private readonly IMailResourceManager _resourceManager;

        //public MailSendTrackingAdapter(IMailResourceManager resourceManager) 
            //=> this._resourceManager = resourceManager; 

        //public async Task<BaseTracking> Get(Guid id) 
            //=> await this._resourceManager.GetMailTracking(id);

        //public async Task Update(BaseTracking tracking) 
            //=> await this._resourceManager.UpdateMailTracking((MailTrackingDto)tracking);
    //}
//}