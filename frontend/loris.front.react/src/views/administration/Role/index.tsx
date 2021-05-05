/* eslint-disable react-hooks/exhaustive-deps */
import React from 'react';
import { useHistory, useLocation } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import TextField from '@material-ui/core/TextField';
import { useTranslation } from 'react-i18next';

import { setSnackbarOn } from '../../../store/snackbar/actions';
import Loading from '../../../components/Loading';
import useStyles from './styles';
import CustomEdit from '../../../components/view/CustomEdit';
import { IRole } from '../../../services/role.d';
import { Grid } from '@material-ui/core';
import { getById, save } from '../../../services/role';
import { getErrorMessage } from '../../../services';
import RoleResources from '../RoleResources';

interface IState {
  id: number;
}

type State = IRole;

const ViewRole: React.FC = () => {
  const [isLoading, setLoading] = React.useState<boolean>(false);
  const [values, setValues] = React.useState<State>({
    id: 0,
    name: '',
    resources: undefined,
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
          history.push('/admin/roles');
        }, 1000);
      })
      .catch(ex => {
        const errorMsg = getErrorMessage(ex);
        dispatch(setSnackbarOn('error', errorMsg));
        setLoading(false);
      });
  };

  const handleCancel = () => {
    history.push('/admin/roles');
  };

  const handleChange = (name: keyof IRole) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setValues({ ...values, [name]: event.target.value });
  };

  React.useEffect(() => {
    const id = (location.state as IState).id;
    //console.log(id);
    handleGetById(id);
  }, []);

  return (
    <div id="roleBox" className={classes.root}>
      <Loading isLoading={isLoading} />
      <CustomEdit
        title={t('lbl_role')}
        rowId={values.id}
        enableSaveButton
        handleSave={handleSave}
        enableCancelButton
        handleCancel={handleCancel}
        component={<RoleResources enabled={false} />}
      >
        <form noValidate>
          <Grid container className={classes.gridContainer}>
            <Grid item xs={12} className={classes.textField}>
              <TextField
                id="name"
                label={t('lbl_role_name')}
                onChange={handleChange('name')}
                value={values.name}
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

export default ViewRole;
