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
import { IResource } from '../../../services/resource.d';
import { Grid } from '@material-ui/core';
import { getById, save } from '../../../services/resource';
import { getErrorMessage } from '../../../services';
//import ResourceResources from '../ResourceResources';

interface IState {
  id: number;
}

type State = IResource;

const ViewResource: React.FC = () => {
  const [isLoading, setLoading] = React.useState<boolean>(false);
  const [values, setValues] = React.useState<State>({
    id: 0,
    code: '',
    dictionary: '',
    description: '',
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
          history.push('/admin/resources');
        }, 1000);
      })
      .catch(ex => {
        const errorMsg = getErrorMessage(ex);
        dispatch(setSnackbarOn('error', errorMsg));
        setLoading(false);
      });
  };

  const handleCancel = () => {
    history.push('/admin/resources');
  };

  const handleChange = (name: keyof IResource) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setValues({ ...values, [name]: event.target.value });
  };

  React.useEffect(() => {
    const id = (location.state as IState).id;
    //console.log(id);
    handleGetById(id);
  }, []);

  return (
    <div id="resourceBox" className={classes.root}>
      <Loading isLoading={isLoading} />
      <CustomEdit
        title={t('lbl_resource')}
        rowId={values.id}
        enableSaveButton
        handleSave={handleSave}
        enableCancelButton
        handleCancel={handleCancel}
      >
        <form noValidate>
          <Grid container className={classes.gridContainer}>
            <Grid item xs={12} className={classes.textField}>
              <TextField
                id="code"
                label={t('lbl_resource_code')}
                onChange={handleChange('code')}
                value={values.code}
                variant="outlined"
                margin="normal"
                required
                fullWidth
              />
            </Grid>
            <Grid item xs={12} className={classes.textField}>
              <TextField
                id="dictionary"
                label={t('lbl_resource_dictionary')}
                onChange={handleChange('dictionary')}
                value={values.dictionary}
                variant="outlined"
                margin="normal"
                required
                fullWidth
              />
            </Grid>
            <Grid item xs={12} className={classes.textField}>
              <TextField
                id="description"
                label={t('lbl_resource_description')}
                onChange={handleChange('description')}
                value={values.description}
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

export default ViewResource;
