import { TSnackbar } from './snackbar.d';

export const SET_SNACKBAR_ON = 'SET_SNACKBAR_ON';
export interface ISetSnackbarOn {
  type: typeof SET_SNACKBAR_ON;
  typeOf: TSnackbar;
  message: string;
}

export const SET_SNACKBAR_OFF = 'SET_SNACKBAR_OFF';
export interface ISetSnackbarOff {
  type: typeof SET_SNACKBAR_OFF;
}

type TActions = ISetSnackbarOn | ISetSnackbarOff;
export default TActions;
