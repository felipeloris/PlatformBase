/* eslint-disable react-hooks/exhaustive-deps */
import React from 'react';
import { useHistory, useLocation } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import TextField from '@material-ui/core/TextField';
import { useTranslation } from 'react-i18next';
import MenuItem from '@material-ui/core/MenuItem';
import IconButton from '@material-ui/core/IconButton';
import Visibility from '@material-ui/icons/Visibility';
import VisibilityOff from '@material-ui/icons/VisibilityOff';

import { setSnackbarOn } from '../../../store/snackbar/actions';
import Loading from '../../../components/Loading';
import useStyles from './styles';
import CustomEdit from '../../../components/view/CustomEdit';
import UserRoles from '../UserRoles';
import { IUser } from '../../../services/user.d';
import { optionsLanguage } from '../../../helpers/languageHelper';
import { Grid } from '@material-ui/core';
import { getById, save } from '../../../services/user';
import { getErrorMessage } from '../../../services';

interface IState {
  id: number;
}

interface State extends IUser {
  showPassword: boolean;
}

const ViewUser: React.FC = () => {
  const [isLoading, setLoading] = React.useState<boolean>(false);
  const [values, setValues] = React.useState<State>({
    id: 0,
    personId: 0,
    extenalId: '',
    password: '',
    showPassword: false,
    nickname: '',
    email: '',
    language: 1,
    note: '',
    roles: undefined,
  });
  const classes = useStyles();
  const dispatch = useDispatch();
  const { t } = useTranslation();
  const history = useHistory();
  const location = useLocation();

  const handleGetById = (id: number) => {
    if (id === 0) return;
    setLoading(true);
    getById(id)
      .then(response => {
        const obj = response.data.result as State;
        obj.password = '';
        obj.showPassword = false;
        setValues(obj);
        setLoading(false);
      })
      .catch(ex => {
        dispatch(setSnackbarOn('error', t('msg_server_unavailable')));
        setLoading(false);
        console.log(ex);
      });
  };

  const handleSave = () => {
    setLoading(true);

    save(values)
      .then(() => {
        setLoading(false);
        dispatch(setSnackbarOn('success', t('msg_record_saved')));
        setTimeout(() => {
          history.push('/admin/users');
        }, 1000);
      })
      .catch(ex => {
        const errorMsg = getErrorMessage(ex);
        dispatch(setSnackbarOn('error', errorMsg));
        setLoading(false);
      });
  };

  const handleCancel = () => {
    history.push('/admin/users');
  };

  const handleChange = (name: keyof IUser) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setValues({ ...values, [name]: event.target.value });
  };

  React.useEffect(() => {
    //const id = toInt((location.state as IState).id).value;
    const id = (location.state as IState).id;
    //console.log(id);
    handleGetById(id);
  }, []);

  const handleClickShowPassword = () => {
    setValues({ ...values, showPassword: !values.showPassword });
  };

  const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
  };

  return (
    <div id="userBox" className={classes.root}>
      <Loading isLoading={isLoading} />
      <CustomEdit
        title={t('lbl_user')}
        rowId={values.id}
        enableSaveButton
        handleSave={handleSave}
        enableCancelButton
        handleCancel={handleCancel}
        component={<UserRoles enabled={false} />}
      >
        <form noValidate>
          <Grid container className={classes.gridContainer}>
            <Grid item sm={5} className={classes.textField}>
              <TextField
                id="extenalId"
                label={t('lbl_user_external_id')}
                onChange={handleChange('extenalId')}
                value={values.extenalId}
                variant="outlined"
                margin="normal"
                required
                fullWidth
              />
            </Grid>
            <Grid item sm={5} className={classes.textField}>
              <TextField
                id="password"
                label={t('lbl_new_password')}
                onChange={handleChange('password')}
                value={values.password}
                variant="outlined"
                margin="normal"
                required
                fullWidth
                type={values.showPassword ? 'text' : 'password'}
              />
            </Grid>
            <Grid item xs={1}>
              <IconButton
                aria-label="toggle password visibility"
                onClick={handleClickShowPassword}
                onMouseDown={handleMouseDownPassword}
                edge="end"
              >
                {values.showPassword ? <Visibility /> : <VisibilityOff />}
              </IconButton>
            </Grid>
            <Grid item xs={5} className={classes.textField}>
              <TextField
                id="nickname"
                label={t('lbl_user_nickname')}
                onChange={handleChange('nickname')}
                value={values.nickname}
                variant="outlined"
                margin="normal"
                required
                fullWidth
              />
            </Grid>
            <Grid item xs={6} className={classes.textField}>
              <TextField
                id="email"
                label={t('lbl_email_address')}
                onChange={handleChange('email')}
                value={values.email}
                variant="outlined"
                margin="normal"
                required
                fullWidth
              />
            </Grid>
            <Grid item xs={5} className={classes.textField}>
              <TextField
                id="language"
                label={t('lbl_user_note')}
                onChange={handleChange('language')}
                value={values.language}
                variant="outlined"
                margin="normal"
                required
                select
                fullWidth
                SelectProps={{
                  MenuProps: {
                    className: classes.menu,
                  },
                }}
              >
                {optionsLanguage.map(option => (
                  <MenuItem key={option.value} value={option.value}>
                    {option.label}
                  </MenuItem>
                ))}
              </TextField>
            </Grid>
            <Grid item xs={6} className={classes.textField}>
              <TextField
                id="note"
                label={t('lbl_user_note')}
                onChange={handleChange('note')}
                value={values.note}
                variant="outlined"
                margin="normal"
                required
                fullWidth
              />
            </Grid>
          </Grid>
        </form>
      </CustomEdit>
    </div>
  );
};

export default ViewUser;
