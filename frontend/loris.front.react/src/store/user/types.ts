import { Action } from 'redux';
import * as sUserType from '../../services/user.d';

//#region Login

export const LOGIN_REQUEST = 'LOGIN_REQUEST';
export interface ILoginRequest extends Action {
  type: typeof LOGIN_REQUEST;
  input: sUserType.ILogin;
  history: any;
}

export const LOGIN_SUCCESS = 'LOGIN_SUCCESS';
export interface ILoginSuccess extends Action {
  type: typeof LOGIN_SUCCESS;
  result: sUserType.IJwtResult;
}

export const LOGIN_FAILURE = 'LOGIN_FAILURE';
export interface ILoginFailure extends Action {
  type: typeof LOGIN_FAILURE;
}

//#endregion

//#region Logout

export const LOGOUT_REQUEST = 'LOGOUT_REQUEST';
export interface ILogoutRequest extends Action {
  type: typeof LOGOUT_REQUEST;
  history: any;
}

export const LOGOUT_SUCCESS = 'LOGOUT_SUCCESS';
export interface ILogoutSuccess extends Action {
  type: typeof LOGOUT_SUCCESS;
}

export const LOGOUT_FAILURE = 'LOGOUT_FAILURE';
export interface ILogoutFailure extends Action {
  type: typeof LOGOUT_FAILURE;
}

//#endregion

//#region Change password

export const CHANGE_PWD_REQUEST = 'CHANGE_PWD_REQUEST';
export interface IChangePwdRequest extends Action {
  type: typeof CHANGE_PWD_REQUEST;
  input: sUserType.IChangePassword;
  history: any;
}

export const CHANGE_PWD_SUCCESS = 'CHANGE_PWD_SUCCESS';
export interface IChangePwdSuccess extends Action {
  type: typeof CHANGE_PWD_SUCCESS;
}

export const CHANGE_PWD_FAILURE = 'CHANGE_PWD_FAILURE';
export interface IChangePwdFailure extends Action {
  type: typeof CHANGE_PWD_FAILURE;
}

//#endregion

//#region Generate Key

export const GENERATE_KEY_REQUEST = 'GENERATE_KEY_REQUEST';
export interface IGenerateKeyRequest extends Action {
  type: typeof GENERATE_KEY_REQUEST;
  input: sUserType.IIdentification;
}

export const GENERATE_KEY_SUCCESS = 'GENERATE_KEY_SUCCESS';
export interface IGenerateKeySuccess extends Action {
  type: typeof GENERATE_KEY_SUCCESS;
}

export const GENERATE_KEY_FAILURE = 'GENERATE_KEY_FAILURE';
export interface IGenerateKeyFailure extends Action {
  type: typeof GENERATE_KEY_FAILURE;
}

//#endregion

//#region Change password with token

export const CHANGE_PWD_WITH_KEY_REQUEST = 'CHANGE_PWD_WITH_KEY_REQUEST';
export interface IChangePwdWithKeyRequest extends Action {
  type: typeof CHANGE_PWD_WITH_KEY_REQUEST;
  input: sUserType.IChangePasswordWithKey;
  history: any;
}

export const CHANGE_PWD_WITH_KEY_SUCCESS = 'CHANGE_PWD_WITH_KEY_SUCCESS';
export interface IChangePwdWithKeySuccess extends Action {
  type: typeof CHANGE_PWD_WITH_KEY_SUCCESS;
}

export const CHANGE_PWD_WITH_KEY_FAILURE = 'CHANGE_PWD_WITH_KEY_FAILURE';
export interface IChangePwdWithKeyFailure extends Action {
  type: typeof CHANGE_PWD_WITH_KEY_FAILURE;
}

//#endregion

export const PERSIST_REHYDRATE = 'persist/REHYDRATE';

type TActions =
  | ILoginRequest
  | ILoginSuccess
  | ILoginFailure
  | ILogoutRequest
  | ILogoutSuccess
  | ILogoutFailure
  | IChangePwdRequest
  | IChangePwdSuccess
  | IChangePwdFailure
  | IGenerateKeyRequest
  | IGenerateKeySuccess
  | IGenerateKeyFailure
  | IChangePwdWithKeyRequest
  | IChangePwdWithKeySuccess
  | IChangePwdWithKeyFailure;
export default TActions;
