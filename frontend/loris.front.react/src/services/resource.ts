import { AxiosResponse } from 'axios';

import { ITreatedResult } from '../common/site.d';
import getAxios from './index';
import * as serviceTypes from './resource.d';

export async function get(): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().get('/Resource', {});
}

export async function getById(id: number): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().get(`/Resource/${id}`, {});
}

export async function del(id: number): Promise<AxiosResponse> {
  return await getAxios().delete(`/Resource/${id}`, {});
}

export async function post(input: serviceTypes.IResource): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().post('/Resource', input);
}

export async function put(input: serviceTypes.IResource): Promise<AxiosResponse<ITreatedResult>> {
  return await getAxios().put('/Resource/0', input);
}

export async function save(input: serviceTypes.IResource): Promise<AxiosResponse<ITreatedResult>> {
  if (input.id > 0) {
    return put(input);
  } else {
    return post(input);
  }
}
