import { AxiosResponse } from 'axios';

import { ITreatedResult } from '../common/site.d';
import getAxios from './index';
import * as serviceTypes from './role.d';

export async function get(): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().get('/Role', {});
}

export async function getById(id: number): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().get(`/Role/${id}`, {});
}

export async function del(id: number): Promise<AxiosResponse> {
  return await getAxios().delete(`/Role/${id}`, {});
}

export async function post(input: serviceTypes.IRole): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().post('/Role', input);
}

export async function put(input: serviceTypes.IRole): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().put('/Role/0', input);
}

export async function save(input: serviceTypes.IRole): Promise<AxiosResponse<ITreatedResult>> {
  if (input.id > 0) {
    return put(input);
  } else {
    return post(input);
  }
}
