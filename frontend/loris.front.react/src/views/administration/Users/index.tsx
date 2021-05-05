/* eslint-disable react-hooks/exhaustive-deps */
import React from 'react';
import { useHistory } from 'react-router-dom';
import {
  GridColDef,
  GridRowId,
  GridRowsProp,
  GridValueFormatterParams,
} from '@material-ui/data-grid';

import Loading from '../../../components/Loading';
import useStyles from './styles';
import i18n from '../../../i18n/config';
import { getLanguageDescription } from '../../../helpers/languageHelper';
import { renderCellExpand } from '../../../components/GridCellExpand';
import { get, del } from '../../../services/user';
import { setSnackbarOn } from '../../../store/snackbar/actions';
import { useDispatch } from 'react-redux';
import CustomGrid from '../../../components/view/CustomGrid';
import { getErrorMessage } from '../../../services';

const columns: GridColDef[] = [
  { field: 'id', headerName: 'ID', width: 70 },
  { field: 'extenalId', headerName: i18n.t('lbl_user_external_id'), width: 150 },
  { field: 'nickname', headerName: i18n.t('lbl_user_nickname'), width: 250 },
  { field: 'email', headerName: i18n.t('lbl_email_address'), width: 250 },
  {
    field: 'language',
    headerName: i18n.t('lbl_user_language'),
    sortable: false,
    width: 140,
    valueFormatter: (params: GridValueFormatterParams) =>
      getLanguageDescription(params.value as number),
  },
  {
    field: 'note',
    headerName: i18n.t('lbl_user_note'),
    width: 200,
    renderCell: renderCellExpand,
  },
];

const ViewUsers: React.FC = () => {
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
      pathname: '/admin/user',
      //search: '?id=0',
      state: { id: 0 },
    });
  };

  const handleEdit = (rowId: GridRowId) => {
    history.push({
      pathname: '/admin/user',
      //search: `?id=${rowId}`,
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
    <div id="userBox" className={classes.root}>
      <Loading isLoading={isLoading} />
      <CustomGrid
        title={i18n.t('lbl_users')}
        rows={rows}
        columns={columns}
        handleAdd={handleAdd}
        handleEdit={handleEdit}
        handleDelete={rowsId => handleDelete(rowsId)}
      />
    </div>
  );
};

export default ViewUsers;
