import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import Snackbar from '@material-ui/core/Snackbar';
import Alert from '@material-ui/lab/Alert';

import { setSnackbarOff } from '../../store/snackbar/actions';
import { ISnackbarWrapper, TSnackbar } from '../../store/snackbar/snackbar';
import useStyles from './styles';

// eslint-disable-next-line @typescript-eslint/explicit-module-boundary-types
const CustomizedSnackbars = () => {
  const classes = useStyles();
  const dispatch = useDispatch();

  const open = useSelector<ISnackbarWrapper, boolean>(state => state.snackbar.open);
  const snackbarType = useSelector<ISnackbarWrapper, TSnackbar>(state => state.snackbar.typeOf);
  const snackbarMessage = useSelector<ISnackbarWrapper, string>(state => state.snackbar.message);

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    dispatch(setSnackbarOff());
  };

  return (
    <div className={classes.root}>
      <Snackbar
        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
        open={open}
        autoHideDuration={3000}
        onClose={handleClose}
      >
        <Alert elevation={6} variant="filled" onClose={handleClose} color={snackbarType}>
          <div>{snackbarMessage}</div>
        </Alert>
      </Snackbar>
    </div>
  );
};

export default CustomizedSnackbars;
