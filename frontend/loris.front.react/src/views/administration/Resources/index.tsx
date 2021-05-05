/* eslint-disable react-hooks/exhaustive-deps */
import React from 'react';
import { useHistory } from 'react-router-dom';
import { GridColDef, GridRowId, GridRowsProp } from '@material-ui/data-grid';

import Loading from '../../../components/Loading';
import useStyles from './styles';
import i18n from '../../../i18n/config';
import { get, del } from '../../../services/resource';
import { setSnackbarOn } from '../../../store/snackbar/actions';
import { useDispatch } from 'react-redux';
import CustomGrid from '../../../components/view/CustomGrid';
import { getErrorMessage } from '../../../services';

const columns: GridColDef[] = [
  { field: 'id', headerName: 'ID', width: 70 },
  { field: 'code', headerName: i18n.t('lbl_code'), width: 150 },
  { field: 'dictionary', headerName: i18n.t('lbl_dictionary'), width: 250 },
  { field: 'description', headerName: i18n.t('lbl_description'), width: 250 },
];

const ViewResources: React.FC = () => {
  const [isLoading, setLoading] = React.useState<boolean>(false);
  const [rows, setRows] = React.useState<GridRowsProp>([]);
  const classes = useStyles();
  const dispatch = useDispatch();
  const history = useHistory();

  const handleGet = () => {
    setLoading(true);
    get()
      .then(response => {
        setRows(response.data.result);
        setLoading(false);
      })
      .catch(ex => {
        const errorMsg = getErrorMessage(ex);
        dispatch(setSnackbarOn('error', errorMsg));
        setLoading(false);
      });
  };

  const handleAdd = () => {
    history.push({
      pathname: '/admin/resource',
      state: { id: 0 },
    });
  };

  const handleEdit = (rowId: GridRowId) => {
    history.push({
      pathname: '/admin/resource',
      state: { id: rowId },
    });
  };

  const handleDelete = (rowsId: GridRowId[]) => {
    setLoading(true);
    del(rowsId[0] as number)
      .then(() => {
        handleGet();
        setLoading(false);
      })
      .catch(ex => {
        const errorMsg = getErrorMessage(ex);
        dispatch(setSnackbarOn('error', errorMsg));
        setLoading(false);
      });
  };

  React.useEffect(() => {
    handleGet();
  }, []);

  return (
    <div id="resourceBox" className={classes.root}>
      <Loading isLoading={isLoading} />
      <CustomGrid
        title={i18n.t('lbl_resources')}
        rows={rows}
        columns={columns}
        handleAdd={handleAdd}
        handleEdit={handleEdit}
        handleDelete={rowsId => handleDelete(rowsId)}
      />
    </div>
  );
};

export default ViewResources;
