import { TLanguage } from '../common/language.d';

export interface IJwtResult {
  accessToken: string;
  tokenExpiresIn: number;
  loginAt: Date;
}

export interface IIdentification {
  identification: string;
  language: TLanguage;
}

export interface ILogin extends IIdentification {
  password: string;
}

export interface IChangePassword extends IIdentification {
  password: string;
  newPassword: string;
}

export interface IChangePasswordWithKey extends IIdentification {
  key: string ;
  newPassword: string;
}

export interface ILoginStatus {
  Undefined: number;
  NotFound: number;
  NotLogged: number;
  Logged: number;
  Blocked: number;
  Disabled: number;
  NotAuthorized: number;
  ExpiredPassword: number;
  InvalidPassword: number;
  ResetPassword: number;
}

export interface IChangePwdStatus {
  Undefined: number,
  InvalidUser: number,
  BlockedUser: number,
  InvalidOldPassword: number,
  InvalidNewPassword: number,
  InvalidNewPasswordEqualOld: number,
  InvalidToken: number,
  GeneratedKey: number,
  PasswordChanged: number,
}

export interface IUser {
  id: number;
  personId?: number;
  extenalId: string;
  password?: string;
  nickname: string;
  email: string;
  language: number;
  note: string;
  roles: any;
}
