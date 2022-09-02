import { BaseDomainDto } from './base-domain-dto';
import { CardTypeDto } from './card-type-dto';
import * as moment from 'moment';

export class PaymentInfoDto extends BaseDomainDto{
    public cardTypeId: number = null;
    public cardNumber: string = null;
    public cvc: string = null;
    public expirationDate: moment.Moment = null;
    public expirationDateModel: string = null;

    public CardType: CardTypeDto = null;

    constructor(){ super(); }
}
