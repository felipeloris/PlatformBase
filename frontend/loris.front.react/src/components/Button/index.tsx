import React from 'react';
import { Button as ButtonBase } from '@material-ui/core';
import { useTheme } from '@material-ui/styles';
import classnames from 'classnames';

import { getColor } from '../../helpers/colorHelper';
import { createStyled } from '../../helpers/styleHelper';
import { CustomTheme } from '../../styles/theme/theme';

interface IProps {
  color?: string;
  className?: string;
  select?: any;
  component?: any;
  href?: string;
  variant?: any;
  onClick?: any;
}

const Button: React.FC<IProps> = props => {
  const theme = useTheme<CustomTheme>();

  const Styled = createStyled({
    root: {
      color: getColor(props.color, theme),
    },
    contained: {
      backgroundColor: getColor(props.color, theme),
      boxShadow: theme.customShadows.widget,
      color: `${props.color ? 'white' : theme.palette.text.primary} !important`,
      '&:hover': {
        backgroundColor: getColor(props.color, theme, 'light'),
        boxShadow: theme.customShadows.widgetWide,
      },
      '&:active': {
        boxShadow: theme.customShadows.widgetWide,
      },
    },
    outlined: {
      color: getColor(props.color, theme),
      borderColor: getColor(props.color, theme),
    },
    select: {
      backgroundColor: theme.palette.primary.main,
      color: '#fff',
    },
  });

  return (
    <Styled>
      {({ classes }) => (
        <ButtonBase
          component={props.component}
          select={props.select}
          href={props.href}
          variant={props.variant}
          onClick={props.onClick}
          classes={{
            contained: classes.contained,
            root: classes.root,
            outlined: classes.outlined,
          }}
          className={classnames(
            {
              [classes.select]: props.select,
            },
            props.className
          )}
        >
          {props.children}
        </ButtonBase>
      )}
    </Styled>
  );
};

export default Button;
