import TActions, * as actionTypes from './types';
import { IUserState } from './user.d';
import { saveAuthorization, deleteAuthorization } from '../../services';
//import * as service from '../../services/user';

const initialState: IUserState = {
  isLoading: false,
  isLogged: false,
  identification: '',
  accessToken: '',
  loginAt: undefined,
};

export default function (state = initialState, action: TActions): IUserState {
  switch (action.type) {
    /**/

    //#region Login

    case actionTypes.LOGIN_REQUEST: {
      const newState = { ...state };
      newState.isLoading = true;
      newState.identification = action.input.identification;
      return newState;
    }

    case actionTypes.LOGIN_SUCCESS: {
      saveAuthorization(action.result.accessToken);

      const newState = { ...state };
      newState.isLoading = false;
      newState.isLogged = true;
      newState.accessToken = action.result.accessToken;
      newState.loginAt = action.result.loginAt;
      return newState;
    }

    case actionTypes.LOGIN_FAILURE: {
      deleteAuthorization();
      const newState = { ...initialState };
      return newState;
    }

    //#endregion

    //#region Logout

    case actionTypes.LOGOUT_REQUEST: {
      const newState = { ...state };
      newState.isLoading = false;
      return newState;
    }

    case actionTypes.LOGOUT_SUCCESS: {
      deleteAuthorization();

      const newState = { ...initialState };
      return newState;
    }

    case actionTypes.LOGOUT_FAILURE: {
      const newState = { ...state };
      newState.isLoading = false;
      return newState;
    }

    //#endregion

    //#region Change password

    case actionTypes.CHANGE_PWD_REQUEST: {
      const newState = { ...state };
      newState.isLoading = true;
      newState.identification = action.input.identification;
      return newState;
    }

    case actionTypes.CHANGE_PWD_SUCCESS: {
      const newState = { ...state };
      newState.isLoading = false;
      return newState;
    }

    case actionTypes.CHANGE_PWD_FAILURE: {
      const newState = { ...state };
      newState.isLoading = false;
      return newState;
    }

    //#endregion

    //#region Generate Key

    case actionTypes.GENERATE_KEY_REQUEST: {
      const newState = { ...state };
      newState.isLoading = true;
      newState.identification = action.input.identification;
      return newState;
    }

    case actionTypes.GENERATE_KEY_SUCCESS: {
      const newState = { ...state };
      newState.isLoading = false;
      return newState;
    }

    case actionTypes.GENERATE_KEY_FAILURE: {
      const newState = { ...state };
      newState.isLoading = false;
      return newState;
    }

    //#endregion

    //#region Change password with Key

    case actionTypes.CHANGE_PWD_WITH_KEY_REQUEST: {
      const newState = { ...state };
      newState.isLoading = true;
      newState.identification = action.input.identification;
      return newState;
    }

    case actionTypes.CHANGE_PWD_WITH_KEY_SUCCESS: {
      const newState = { ...state };
      newState.isLoading = false;
      return newState;
    }

    case actionTypes.CHANGE_PWD_WITH_KEY_FAILURE: {
      const newState = { ...state };
      newState.isLoading = false;
      return newState;
    }

    //#endregion

    default:
      return state;
  }
}
