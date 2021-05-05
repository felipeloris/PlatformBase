import { AxiosResponse } from 'axios';

import getAxios from './index';
import { ITreatedResult } from '../common/site.d';
import * as serviceTypes from './user.d';

//#region 'Enums' do C#

export const LoginStatus: serviceTypes.ILoginStatus = {
  Undefined: 0,
  NotFound: 1,
  NotLogged: 2,
  Logged: 3,
  Blocked: 4,
  Disabled: 5,
  NotAuthorized: 6,
  ExpiredPassword: 7,
  InvalidPassword: 8,
  ResetPassword: 9,
};

export type TLoginStatus = typeof LoginStatus[keyof typeof LoginStatus];

export const ChangePwdStatus: serviceTypes.IChangePwdStatus = {
  Undefined: 0,
  InvalidUser: 1,
  BlockedUser: 2,
  InvalidOldPassword: 3,
  InvalidNewPassword: 4,
  InvalidNewPasswordEqualOld: 5,
  InvalidToken: 6,
  GeneratedKey: 7,
  PasswordChanged: 8,
};

export type TChangePwdStatus = typeof ChangePwdStatus[keyof typeof ChangePwdStatus];

//#endregion

export async function login(input: serviceTypes.ILogin): Promise<AxiosResponse<ITreatedResult>> {
  const params = { ...input };
  return await getAxios().post('/User/Login', {}, { params });
}

export async function logout(): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().post('/User/Logout');
}

export async function changePassword(
  input: serviceTypes.IChangePassword
): Promise<AxiosResponse<ITreatedResult>> {
  const params = { ...input };
  return await getAxios().post('/User/ChangePassword', {}, { params });
}

export async function generateKey(
  input: serviceTypes.IIdentification
): Promise<AxiosResponse<ITreatedResult>> {
  const params = { ...input };
  return await getAxios().post('/User/GenerateKey', {}, { params });
}

export async function changePasswordWithKey(
  input: serviceTypes.IChangePasswordWithKey
): Promise<AxiosResponse<ITreatedResult>> {
  const params = { ...input };
  return await getAxios().post('/User/ChangePasswordWithToken', {}, { params });
}

export async function get(): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().get('/User', {});
}

export async function getById(id: number): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().get(`/User/${id}`, {});
}

export async function del(id: number): Promise<AxiosResponse> {
  return await getAxios().delete(`/User/${id}`, {});
}

export async function post(input: serviceTypes.IUser): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().post('/User', input);
}

export async function put(input: serviceTypes.IUser): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().put('/User/0', input);
}

export async function save(input: serviceTypes.IUser): Promise<AxiosResponse<ITreatedResult>> {
  if (input.id > 0) {
    return put(input);
  } else {
    return post(input);
  }
}
