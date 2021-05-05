import { makeStyles } from '@material-ui/styles';

import { CustomTheme } from '../../styles/theme/theme';
import { getColor } from '../../helpers/colorHelper';

interface IProps {
  color?: string;
  className?: string;
  select?: any;
  component?: any;
  href?: string;
  variant?: any;
  onClick?: any;
}

export default makeStyles<CustomTheme>(theme => ({
  root: {
    color: (props: IProps) => getColor(props.color, theme),
  },
  contained: {
    backgroundColor: (props: IProps) => getColor(props.color, theme),
    boxShadow: theme.customShadows.widget,
    color: (props: IProps) => `${props.color ? 'white' : theme.palette.text.primary} !important`,
    '&:hover': {
      backgroundColor: (props: IProps) => getColor(props.color, theme, 'light'),
      boxShadow: theme.customShadows.widgetWide,
    },
    '&:active': {
      boxShadow: theme.customShadows.widgetWide,
    },
  },
  outlined: {
    color: (props: IProps) => getColor(props.color, theme),
    borderColor: (props: IProps) => getColor(props.color, theme),
  },
  select: {
    backgroundColor: theme.palette.primary.main,
    color: '#fff',
  },
}));
