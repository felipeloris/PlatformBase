import axios from 'axios';
import i18n from '../i18n/config';

// eslint-disable-next-line @typescript-eslint/explicit-module-boundary-types
const getAxios = () => {
  axios.defaults.timeout = 10000;
  axios.defaults.timeoutErrorMessage = i18n.t('msg_server_unavailable');

  const obj = axios.create({
    baseURL: 'https://localhost:5001/api',
    responseType: 'json',
    headers: {
      'Content-Type': 'application/json',
      //Authorization: `Bearer ${localStorage.getItem('token')}`,
    },
  });

  /*
  obj.interceptors.response.use(
    response => {
      return response;
    },
    error => {
      if (error.response.status === 401) {
        deleteAuthorization();
      }
      return error;
    }
  );
  */

  return obj;
};

export const saveAuthorization = (token: string): void => {
  //console.log('set authorization');
  axios.defaults.headers.Authorization = `Bearer ${token}`;
  //localStorage.setItem('token', token);
};

export const deleteAuthorization = (): void => {
  //console.log('delete authorization');
  delete axios.defaults.headers.Authorization;
  //localStorage.removeItem('token');
};

export const getErrorMessage = (ex: any): string => {
  /*
  400: Bad request
  401: Unauthorized
  403: Forbidden
  404: Not Found
  500: Internal Server error
  502: Bad Gateway
  */
  const defaultErrorMsg = i18n.t('msg_server_unavailable');

  if (ex.response != null && ex.response.data != null) {
    //console.log(ex.response);
    const data = ex.response.data;
    // TreatedResult (treated exception on C# - can be business exception)
    if (data.message != null) {
      return data.message;
    }

    // Validation exception
    if (data.errors != null) {
      let errors = '';
      const propNames = Object.getOwnPropertyNames(data.errors);
      if (propNames != null) {
        propNames.forEach(function (propName) {
          //console.log('name: ' + propName + ' value: ' + data.errors[propName]);
          errors += data.errors[propName];
        });
      }

      return errors;
    }
  }

  return defaultErrorMsg;
};

export default getAxios;
