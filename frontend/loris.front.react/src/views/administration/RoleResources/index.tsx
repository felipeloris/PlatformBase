import React from 'react';
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionActions';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { useTranslation } from 'react-i18next';

interface IRoleResourcesProps {
  enabled: boolean;
}

const RoleResources: React.FC<IRoleResourcesProps> = props => {
  const { t } = useTranslation();

  return (
    <Accordion disabled={!props.enabled}>
      <AccordionSummary
        expandIcon={<ExpandMoreIcon />}
        aria-controls="panel1a-content"
        id="panel1a-header"
      >
        {t('lbl_resources')}
      </AccordionSummary>
      <AccordionDetails></AccordionDetails>
    </Accordion>
  );
};

export default RoleResources;
