import React from 'react';
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionActions';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { useTranslation } from 'react-i18next';

interface IUserRolesProps {
  enabled: boolean;
}

const UserRoles: React.FC<IUserRolesProps> = props => {
  const { t } = useTranslation();

  return (
    <Accordion disabled={!props.enabled}>
      <AccordionSummary
        expandIcon={<ExpandMoreIcon />}
        aria-controls="panel1a-content"
        id="panel1a-header"
      >
        {t('lbl_roles')}
      </AccordionSummary>
      <AccordionDetails></AccordionDetails>
    </Accordion>
  );
};

export default UserRoles;
