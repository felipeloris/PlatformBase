import { call, put, all, takeLatest } from 'redux-saga/effects';
import { get } from 'lodash';
import { getErrorMessage, saveAuthorization } from '../../services';

import { setSnackbarOn } from '../snackbar/actions';
import * as actions from './actions';
import * as actionTypes from './types';
import * as service from '../../services/user';
import { ITreatedResult } from '../../common/site.d';
import { TreatedResultStatus } from '../../common/site';

const goView = function* (history: any, urlView: string) {
  const runner = yield call(
    setInterval,
    () => {
      history.push(urlView);
    },
    1000
  );
  return runner;
  //yield delay(500);
};

//#region Login

const login = function* ({ input, history }: actionTypes.ILoginRequest) {
  try {
    const response = yield call(service.login, input);
    const tResult = response.data as ITreatedResult;

    if (tResult.status !== TreatedResultStatus.Success) {
      throw new Error(tResult.message);
    }

    yield put(setSnackbarOn('success', tResult.message));
    yield put(actions.loginSuccess({ ...tResult.result }));
    yield goView(history, '/app');
  } catch (e) {
    console.log({ error: e });
    const errorMsg = getErrorMessage(e);
    yield put(actions.loginFailure());
    yield put(setSnackbarOn('error', errorMsg));
  }
};

//#endregion

//#region Logout

const logout = function* ({ history }: actionTypes.ILogoutRequest) {
  try {
    const response = yield call(service.logout);
    const tResult = response.data as ITreatedResult;

    if (tResult.status !== TreatedResultStatus.Success) {
      throw new Error(tResult.message);
    }

    yield put(actions.logoutSuccess());
    history.push('/');
  } catch (e) {
    const errorMsg = getErrorMessage(e);
    yield put(actions.logoutFailure());
    yield put(setSnackbarOn('error', errorMsg));
  }
};

//#endregion

//#region Change password

const changePassword = function* ({ input, history }: actionTypes.IChangePwdRequest) {
  try {
    const response = yield call(service.changePassword, input);
    const tResult = response.data as ITreatedResult;

    console.log({ tResult });

    if (tResult.status !== TreatedResultStatus.Success) {
      throw new Error(tResult.message);
    }

    yield put(setSnackbarOn('success', tResult.message));
    yield put(actions.ChangePasswordSuccess());
    yield goView(history, '/signin');
  } catch (e) {
    const errorMsg = getErrorMessage(e);
    yield put(actions.ChangePasswordFailure());
    yield put(setSnackbarOn('error', errorMsg));
  }
};

//#endregion

//#region Generate Key
const generateKey = function* ({ input }: actionTypes.IGenerateKeyRequest) {
  try {
    const response = yield call(service.generateKey, input);
    const tResult = response.data as ITreatedResult;

    if (tResult.status !== TreatedResultStatus.Success) {
      throw new Error(tResult.message);
    }

    yield put(setSnackbarOn('success', tResult.message));
    yield put(actions.GenerateKeySuccess());
  } catch (e) {
    const errorMsg = getErrorMessage(e);
    yield put(actions.GenerateKeyFailure());
    yield put(setSnackbarOn('error', errorMsg));
  }
};

//#endregion

//#region Change password with Key
const changePasswordWithKey = function* ({ input, history }: actionTypes.IChangePwdWithKeyRequest) {
  try {
    const response = yield call(service.changePasswordWithKey, input);
    const tResult = response.data as ITreatedResult;

    if (tResult.status !== TreatedResultStatus.Success) {
      throw new Error(tResult.message);
    }

    yield put(setSnackbarOn('success', tResult.message));
    yield put(actions.ChangePasswordSuccess());
    yield goView(history, '/signin');
  } catch (e) {
    const errorMsg = getErrorMessage(e);
    yield put(actions.ChangePasswordWithKeyFailure());
    yield put(setSnackbarOn('error', errorMsg));
  }
};

//#endregion

const persistRehydrate = ({ payload }: any) => {
  const token = get(payload, 'user.accessToken', '');
  if (!token) return;
  saveAuthorization(token);
};

export default all([
  takeLatest(actionTypes.LOGIN_REQUEST, login),
  takeLatest(actionTypes.LOGOUT_REQUEST, logout),
  takeLatest(actionTypes.CHANGE_PWD_REQUEST, changePassword),
  takeLatest(actionTypes.GENERATE_KEY_REQUEST, generateKey),
  takeLatest(actionTypes.CHANGE_PWD_WITH_KEY_REQUEST, changePasswordWithKey),
  takeLatest(actionTypes.PERSIST_REHYDRATE, persistRehydrate),
]);
