export type TSnackbar = 'error' | 'warning' | 'info' | 'success';

export interface ISnackbarState {
  open: boolean;
  typeOf: TSnackbar;
  message: string;
}

interface ISnackbarWrapper {
  snackbar: ISnackbarState;
}
