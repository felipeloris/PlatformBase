import * as actionTypes from './types';
import { TSnackbar } from './snackbar.d';

export const setSnackbarOn = (typeOf: TSnackbar, message: string): actionTypes.ISetSnackbarOn => ({
  type: actionTypes.SET_SNACKBAR_ON,
  typeOf,
  message,
});

export const setSnackbarOff = (): actionTypes.ISetSnackbarOff => ({
  type: actionTypes.SET_SNACKBAR_OFF,
});
