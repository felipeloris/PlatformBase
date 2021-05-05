//import { Overrides } from '@material-ui/core/styles/overrides';

import MuiButton from './MuiButton';
import MuiIconButton from './MuiIconButton';
import MuiPaper from './MuiPaper';
import MuiTableCell from './MuiTableCell';
import MuiTableHead from './MuiTableHead';
import MuiTypography from './MuiTypography';

//const overrides = (): Overrides => {
const overrides = (): any => {
  return {
    MuiButton,
    MuiIconButton,
    MuiPaper,
    MuiTableCell,
    MuiTableHead,
    MuiTypography,
  };
};

export default overrides;
