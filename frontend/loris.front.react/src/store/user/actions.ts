/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
import * as sUserTypes from '../../services/user.d';
import * as actionTypes from './types';

//#region Login

export const loginRequest = (input: sUserTypes.ILogin, history: any): actionTypes.ILoginRequest => {
  return {
    type: actionTypes.LOGIN_REQUEST,
    input,
    history,
  };
};

export const loginSuccess = (result: sUserTypes.IJwtResult): actionTypes.ILoginSuccess => {
  return {
    type: actionTypes.LOGIN_SUCCESS,
    result,
  };
};

export const loginFailure = (): actionTypes.ILoginFailure => {
  return {
    type: actionTypes.LOGIN_FAILURE,
  };
};

//#endregion

//#region Logout

export const logoutRequest = (history: any): actionTypes.ILogoutRequest => {
  return {
    type: actionTypes.LOGOUT_REQUEST,
    history,
  };
};

export const logoutSuccess = (): actionTypes.ILogoutSuccess => {
  return {
    type: actionTypes.LOGOUT_SUCCESS,
  };
};

export const logoutFailure = (): actionTypes.ILogoutFailure => {
  return {
    type: actionTypes.LOGOUT_FAILURE,
  };
};

//#endregion

//#region Change password

export const ChangePasswordRequest = (
  input: sUserTypes.IChangePassword,
  history: any
): actionTypes.IChangePwdRequest => {
  return {
    type: actionTypes.CHANGE_PWD_REQUEST,
    input,
    history,
  };
};

export const ChangePasswordSuccess = (): actionTypes.IChangePwdSuccess => {
  return {
    type: actionTypes.CHANGE_PWD_SUCCESS,
  };
};

export const ChangePasswordFailure = (): actionTypes.IChangePwdFailure => {
  return {
    type: actionTypes.CHANGE_PWD_FAILURE,
  };
};

//#endregion

//#region Generate Key

export const GenerateKeyRequest = (
  input: sUserTypes.IIdentification
): actionTypes.IGenerateKeyRequest => {
  return {
    type: actionTypes.GENERATE_KEY_REQUEST,
    input,
  };
};

export const GenerateKeySuccess = (): actionTypes.IGenerateKeySuccess => {
  return {
    type: actionTypes.GENERATE_KEY_SUCCESS,
  };
};

export const GenerateKeyFailure = (): actionTypes.IGenerateKeyFailure => {
  return {
    type: actionTypes.GENERATE_KEY_FAILURE,
  };
};

//#endregion

//#region Change password with Key

export const ChangePasswordWithKeyRequest = (
  input: sUserTypes.IChangePasswordWithKey,
  history: any
): actionTypes.IChangePwdWithKeyRequest => {
  return {
    type: actionTypes.CHANGE_PWD_WITH_KEY_REQUEST,
    input,
    history,
  };
};

export const ChangePasswordWithKeySuccess = (): actionTypes.IChangePwdWithKeySuccess => {
  return {
    type: actionTypes.CHANGE_PWD_WITH_KEY_SUCCESS,
  };
};

export const ChangePasswordWithKeyFailure = (): actionTypes.IChangePwdWithKeyFailure => {
  return {
    type: actionTypes.CHANGE_PWD_WITH_KEY_FAILURE,
  };
};

//#endregion
