import { BaseDomainDto } from './base-domain-dto';

export class BaseExtDto extends BaseDomainDto{

    public payload: string = null;

    //public deserialize<T>(): T{
        //return JSON.parse(this.payload) as T;
    //}
}
