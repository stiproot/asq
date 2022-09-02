import { BaseDomainDto } from './base-domain-dto';
import { AccountTypeEnu } from './account-type-enu';
import { FocusUserMappingDto } from './focus-user-mapping-dto';
import { HostDto } from './host-dto';
import { ImgDto } from './img-dto';
import { PaymentInfoDto } from './payment-info-dto';

export class UserDto extends BaseDomainDto{
    public username: string = null;
    public accountType: AccountTypeEnu = AccountTypeEnu.STUDENT;
    public password: string = null;
    public email: string = null;
    public name: string = null;
    public surname: string = null;
    public paymentInfoId: number | null = null;
    public hostId: number | null = 0;
    public imgId: number = 0;
    public timezoneId: number = 0;
    public interests: FocusUserMappingDto[] = null;

    public host: HostDto = null;
    public paymentInfo: PaymentInfoDto = null;
    public img: ImgDto = null;

    //public passwordConfirm: string = null;
    public token: string = null;
    public isHost: boolean = false;
    public isContractSigned: boolean = null;

    constructor(){
        super();
    }
}
