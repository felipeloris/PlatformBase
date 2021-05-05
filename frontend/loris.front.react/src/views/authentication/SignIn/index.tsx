import React from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import Link from '@material-ui/core/Link';
import Paper from '@material-ui/core/Paper';
import Box from '@material-ui/core/Box';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import Visibility from '@material-ui/icons/Visibility';
import VisibilityOff from '@material-ui/icons/VisibilityOff';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import { useTranslation } from 'react-i18next';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { connect, ConnectedProps } from 'react-redux';
import { get } from 'lodash';

import { useChangeTheme, isLight } from '../../../hooks/theme';
import { useLanguage } from '../../../hooks/language';
import Flag from '../../../components/Flag';
import Copyright from '../../../components/Copyright';
import { loginRequest } from '../../../store/user/actions';
import { setSnackbarOn } from '../../../store/snackbar/actions';
import Loading from '../../../components/Loading';
import { AuthenticationValidations } from '../../../common/validations';
import useStyles from './styles';
import { IUserState } from '../../../store/user/user';

const mapStateToProps = (state: { user: IUserState }) => ({
  isLoading: state.user.isLoading,
});
const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) =>
  bindActionCreators({ setSnackbarOn, loginRequest }, dispatch);
const connector = connect(mapStateToProps, mapDispatchToProps);
type HeaderProps = ConnectedProps<typeof connector>;

//interface IProps extends HeaderProps {}
interface State {
  identification: string;
  password: string;
  showPassword: boolean;
}

const SignIn: React.FC<HeaderProps> = props => {
  const classes = useStyles();
  const { t } = useTranslation();
  const { themeType, handleThemeChange } = useChangeTheme();
  const { language } = useLanguage();
  const [values, setValues] = React.useState<State>({
    identification: '',
    password: '',
    showPassword: false,
  });
  const identificationRef = React.useRef(null);
  const passwordRef = React.useRef(null);

  const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setValues({ ...values, [prop]: event.target.value });
  };

  const handleSubmit = (e: { preventDefault: () => void }) => {
    e.preventDefault();

    const validations = new AuthenticationValidations();
    validations.identification(identificationRef);
    validations.password(passwordRef, '"senha"');

    if (validations.hasError()) {
      props.setSnackbarOn('error', validations.getMessageFirst());
      validations.setFocusFirst();
      return;
    }

    const history = get(props, 'history');
    //const prevPath = get(props, 'location.state.prevPath', '/');

    props.loginRequest(
      { identification: values.identification, password: values.password, language },
      history
    );
  };

  const handleClickShowPassword = () => {
    setValues({ ...values, showPassword: !values.showPassword });
  };

  const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
  };

  return (
    <Grid container component="main" className={classes.root}>
      <Loading isLoading={props.isLoading} />
      <Grid item xs={false} sm={5} md={7} className={classes.image} />
      <Grid item xs={12} sm={7} md={5} component={Paper} elevation={6} square>
        <div className={classes.paper}>
          <Avatar className={classes.avatar}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Login
          </Typography>
          <form className={classes.form} noValidate onSubmit={handleSubmit}>
            <Grid container alignItems="center" justify="center">
              <Grid item xs={12}>
                <TextField
                  id="identification"
                  name="identification"
                  label={t('lbl_identification')}
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  autoFocus
                  inputRef={identificationRef}
                  onChange={handleChange('identification')}
                />
              </Grid>
              <Grid item xs={11}>
                <TextField
                  id="password"
                  name="password"
                  label={t('lbl_user_pwd')}
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  type={values.showPassword ? 'text' : 'password'}
                  autoComplete="current-password"
                  inputRef={passwordRef}
                  onChange={handleChange('password')}
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
            </Grid>
            <div className={classes.config}>
              <FormControlLabel
                control={<Checkbox value="remember" color="primary" />}
                label={t('lbl_remember_me')}
              />
              <FormControlLabel
                control={
                  <Checkbox
                    checked={!isLight(themeType)}
                    onChange={handleThemeChange}
                    color="primary"
                  />
                }
                label={t('lbl_theme_dark')}
                labelPlacement="end"
              />
              <div className={classes.flagBox}>
                <Flag flagLanguage={'portuguese'} />
                <Flag flagLanguage={'english'} />
                <Flag flagLanguage={'spanish'} />
              </div>
            </div>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={classes.submit}
            >
              {t('lbl_enter')}
            </Button>
            <Grid container>
              <Grid item xs={6} style={{ textAlign: 'left', backgroundColor: 'inherit' }}>
                <Link href="/password_forgot" variant="body2">
                  {t('lbl_forgot_password')}
                </Link>
              </Grid>
              <Grid item xs={6} style={{ textAlign: 'right', backgroundColor: 'inherit' }}>
                <Link href="/password_change" variant="body2">
                  {t('lbl_change_pwd')}
                </Link>
              </Grid>
            </Grid>
            <Box mt={5}>
              <Copyright />
            </Box>
          </form>
        </div>
      </Grid>
    </Grid>
  );
};

export default connector(SignIn);
