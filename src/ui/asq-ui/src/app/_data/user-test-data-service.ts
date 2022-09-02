import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { UserDto } from '../_models/domain/user-dto';

@Injectable({
  providedIn: 'root'
})
export class UserTestDataService {

  constructor() { }

  private testUser =
  {
      'username': 'stiphost',
      'password': 'stip',
      'email': 'stiphost@gmail.com',
      'name': 'stiphost',
      'surname': 'stiphost',
      'paymentInfoId': 15,
      'hostId': 16,
      'imgId': 16,
      'timezoneId': 1,
      'accountType': 1,
      'paymentInfo': {
          'cardNumber': '789978789',
          'expirationDate': '03/03',
          'cvc': '789',
          'cardTypeId': 3,
          'cardType': null,
          'id': 15,
          'uniqueId': '162d69a4-9086-435c-abc2-19581279b8c6',
          'creationDateUtc': '2020-10-28T08:49:20.607',
          'creationUserId': 0,
          'inactive': false
      },
      'host': {
          'company': 'I work here',
          'careerSummary': 'I worked here',
          'extUserId': 6,
          'specialities': [
              {
                  'hostId': 16,
                  'focusId': 2,
                  'focus': {
                      'description': 'Construction',
                      'id': 2,
                      'uniqueId': '73031c91-ecd2-49bf-9850-4847e1484dd3',
                      'creationDateUtc': '2020-10-11T14:23:41.453',
                      'creationUserId': 0,
                      'inactive': false
                  },
                  'id': 14,
                  'uniqueId': '9d2fdf10-d3d7-4e93-92c6-ad6504e9e6bd',
                  'creationDateUtc': '2020-10-28T08:50:45.033',
                  'creationUserId': 0,
                  'inactive': false
              }
          ],
          'extUser': {
              'hostId': 0,
              'deserializedPayload': {
                  'id': '6A42KiD5SoGVA2z-ru9jSw',
                  'email': 'simon@asq.properties.co.za',
                  'type': 1,
                  'first_name': 'ash',
                  'last_name': 'catchum'
              },
              'payload': '{\'id\':\'6A42KiD5SoGVA2z-ru9jSw\',\'email\':\'simon@asq.properties.co.za\',\'type\':1,\'first_name\':\'ash\',\'last_name\':\'catchum\'}',
              'id': 6,
              'uniqueId': 'f710758b-e1ac-4e02-b131-24812a256240',
              'creationDateUtc': '2020-10-28T08:51:44.797',
              'creationUserId': 0,
              'inactive': false
          },
          'id': 16,
          'uniqueId': 'dda88c25-e8a6-44dd-a41f-be372170c3d6',
          'creationDateUtc': '2020-10-28T08:49:20.607',
          'creationUserId': 0,
          'inactive': false
      },
      'img': {
          'data': '',
          'fileName': 'profile.png',
          'path': 'http://localhost:3000/image/317bca08-51b8-41fa-a0e2-b646655b5923/profile.png',
          'thumbnailUrl': 'http://localhost:3000/image/317bca08-51b8-41fa-a0e2-b646655b5923/profile.png',
          'id': 16,
          'uniqueId': 'c3195d4e-4f73-4986-aa6e-383b9b3baa18',
          'creationDateUtc': '2020-10-28T08:50:14.997',
          'creationUserId': 0,
          'inactive': false
      },
      'timezone': {
          'display': 'Midway Island, Samoa',
          'utcOffset': 13,
          'extCode': 'Pacific/Midway',
          'id': 1,
          'uniqueId': 'f63da6ce-d813-4ba1-a43f-b09207252a2d',
          'creationDateUtc': '2020-11-09T14:15:45.11',
          'creationUserId': 0,
          'inactive': false
      },
      'interests': [
          {
              'userId': 15,
              'focusId': 1,
              'focus': {
                  'description': 'Housing & Deveopment',
                  'id': 1,
                  'uniqueId': '87792284-e67c-4f2d-8822-7a2ee4a6d458',
                  'creationDateUtc': '2020-10-11T14:23:40.973',
                  'creationUserId': 0,
                  'inactive': false
              },
              'id': 12,
              'uniqueId': '423da353-10fe-4038-a987-feb215adc6c1',
              'creationDateUtc': '2020-10-28T08:49:42.207',
              'creationUserId': 0,
              'inactive': false
          }
      ],
      'token': null,
      'id': 15,
      'uniqueId': '317bca08-51b8-41fa-a0e2-b646655b5923',
      'creationDateUtc': '2020-10-28T08:49:20.607',
      'creationUserId': 0,
      'inactive': false,
      'isHost': true,
      'isContractSigned': true
  };

  public user(): Observable<UserDto>{
      return of(JSON.parse(JSON.stringify(this.testUser)));
  }
}
