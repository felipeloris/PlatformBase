import TActions, * as actionTypes from './types';
import { ISnackbarState } from './snackbar.d';

const initialState: ISnackbarState = {
  open: false,
  typeOf: 'success',
  message: '',
};

export default (state = initialState, action: TActions): ISnackbarState => {
  switch (action.type) {
    case actionTypes.SET_SNACKBAR_ON: {
      const newState = { ...state };
      newState.open = true;
      newState.typeOf = action.typeOf;
      newState.message = action.message;
      return newState;
    }

    case actionTypes.SET_SNACKBAR_OFF: {
      const newState = { ...state };
      newState.open = false;
      return newState;
    }

    default:
      return state;
  }
};
