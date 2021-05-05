export interface IUserState {
  isLoading: boolean;
  isLogged: boolean,
  identification: string;
  accessToken: string;
  loginAt?: Date;
}

export interface IUserStateWrapper {
  user: IUserState;
}
