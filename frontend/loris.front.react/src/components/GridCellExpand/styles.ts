/* eslint-disable @typescript-eslint/no-unused-vars */
import { makeStyles } from '@material-ui/core/styles';

export default makeStyles(theme => ({
  root: {
    alignItems: 'center',
    lineHeight: '24px',
    width: '100%',
    height: '100%',
    position: 'relative',
    display: 'flex',
    '& .cellValue': {
      whiteSpace: 'nowrap',
      overflow: 'hidden',
      textOverflow: 'ellipsis',
    },
  },
}));
