/* eslint-disable react/jsx-key */
import React from 'react';
//import { useDispatch } from 'react-redux';
import SaveIcon from '@material-ui/icons/Save';
import CancelIcon from '@material-ui/icons/Cancel';
import Grid from '@material-ui/core/Grid';
import Accordion from '@material-ui/core/Accordion';
import AccordionDetails from '@material-ui/core/AccordionActions';
import { useTranslation } from 'react-i18next';

import useStyles from './styles';
import CustomButton from '../CustomButton';
//import { setSnackbarOn } from '../../../store/snackbar/actions';

interface ICustomEditProps {
  title: string;
  rowId: number;
  enableSaveButton: boolean;
  handleSave(): void;
  enableCancelButton: boolean;
  handleCancel(): void;
  component?: React.ReactNode;
}

const CustomEdit: React.FC<ICustomEditProps> = props => {
  //const dispatch = useDispatch();
  const classes = useStyles();
  const { t } = useTranslation();

  return (
    <div id="boxDataGrid" className={classes.container}>
      <h1 className={classes.title}>{`${props.title} - ${
        props.rowId > 0 ? t('lbl_item_edt') : t('lbl_item_add')
      }`}</h1>
      <Accordion>
        <AccordionDetails>
          <Grid container direction="column" justify="flex-start" alignItems="flex-start">
            <Grid item>{props.children}</Grid>
            <Grid item>
              <Grid container className={classes.commandBox} spacing={3}>
                <Grid item>
                  <CustomButton
                    className={classes.commandButton}
                    variant="contained"
                    startIcon={<SaveIcon />}
                    disabled={!props.enableSaveButton}
                    onClick={() => props.handleSave()}
                  >
                    {t('lbl_save')}
                  </CustomButton>
                </Grid>
                <Grid item>
                  <CustomButton
                    className={classes.commandButton}
                    variant="contained"
                    startIcon={<CancelIcon />}
                    disabled={!props.enableCancelButton}
                    onClick={() => props.handleCancel()}
                  >
                    {t('lbl_cancel')}
                  </CustomButton>
                </Grid>
              </Grid>
            </Grid>
          </Grid>
        </AccordionDetails>
      </Accordion>
      {props.component}
    </div>
  );
};

export default CustomEdit;
