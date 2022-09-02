import { FocusUserMappingDto } from '../domain/focus-user-mapping-dto';

export class BasicUserModel{
    public Username: string;
    public Email: string;
    public Name: string;
    public Surname: string;
    public Password: string;
    public PasswordConfirm: string;
    public isHost: boolean;
    public Interests: FocusUserMappingDto[];
}
