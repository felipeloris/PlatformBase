/* eslint-disable react/jsx-key */
import React from 'react';
import { useDispatch } from 'react-redux';
import AddIcon from '@material-ui/icons/Add';
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Grid from '@material-ui/core/Grid';
import { DataGrid, GridColumns, GridRowId, GridRowsProp } from '@material-ui/data-grid';
import { useTranslation } from 'react-i18next';

import useStyles from './styles';
import CustomButton from '../CustomButton';
import { setSnackbarOn } from '../../../store/snackbar/actions';

interface ICustomGridProps {
  title: string;
  rows: GridRowsProp;
  columns: GridColumns;
  handleAdd(): void;
  handleEdit(rowId: GridRowId): void;
  handleDelete(rowsId: GridRowId[]): void;
}

/*
Todo:
- trocar por table (https://next.material-ui.com/components/tables/)
*/

const CustomGrid: React.FC<ICustomGridProps> = props => {
  const [selectedId, setSelectedId] = React.useState<GridRowId[]>([]);
  const [openDialog, setOpenDialog] = React.useState<boolean>(false);
  const dispatch = useDispatch();
  const classes = useStyles();
  const { t } = useTranslation();

  const handleCallDelete = () => {
    props.handleDelete(selectedId);
    handleCloseDialog();
  };

  const handleOpenDialog = () => {
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setOpenDialog(false);
  };

  return (
    <div id="boxDataGrid" className={classes.container}>
      <Dialog
        open={openDialog}
        onClose={handleCloseDialog}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">{t('lbl_to_confirm')}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            {t('msg_conf_delete_record')}
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCallDelete} color="primary">
            Ok
          </Button>
          <Button onClick={handleCloseDialog} color="primary" autoFocus>
            {t('lbl_cancel')}
          </Button>
        </DialogActions>
      </Dialog>
      <h1 className={classes.title}>{props.title}</h1>
      <Grid container className={classes.commandBox} spacing={3}>
        <Grid item>
          <CustomButton
            className={classes.commandButton}
            variant="contained"
            startIcon={<AddIcon />}
            onClick={() => props.handleAdd()}
          >
            {t('lbl_item_add')}
          </CustomButton>
        </Grid>
        <Grid item>
          <CustomButton
            className={classes.commandButton}
            variant="contained"
            startIcon={<EditIcon />}
            onClick={() => {
              if (selectedId.length !== 1) {
                dispatch(setSnackbarOn('error', t('msg_one_record_to_edit')));
                return;
              }
              props.handleEdit(selectedId[0]);
            }}
          >
            {t('lbl_item_edt')}
          </CustomButton>
        </Grid>
        <Grid item>
          <CustomButton
            className={classes.commandButton}
            variant="contained"
            startIcon={<DeleteIcon />}
            onClick={() => {
              if (selectedId.length === 0) {
                dispatch(setSnackbarOn('error', t('msg_one_record_to_delete')));
                return;
              }
              handleOpenDialog();
            }}
          >
            {t('lbl_item_del')}
          </CustomButton>
        </Grid>
      </Grid>
      <DataGrid
        className={classes.dataGrid}
        rows={props.rows}
        columns={props.columns}
        checkboxSelection
        onSelectionModelChange={newSelection => {
          setSelectedId(newSelection.selectionModel);
        }}
        hideFooterPagination
      />
    </div>
  );
};

export default CustomGrid;
