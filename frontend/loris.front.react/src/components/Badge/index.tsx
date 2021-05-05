import React from 'react';
import { Theme, Badge as BadgeBase } from '@material-ui/core';
import { useTheme } from '@material-ui/styles';
import classnames from 'classnames';

import { getColor } from '../../helpers/colorHelper';
import { useStyles, createStyled } from '../../helpers/styleHelper';

interface IProps {
  colorBrightness?: string;
  color?: string;
  badgeContent?: any;
  colortheme?: string;
}

const Badge: React.FC<IProps> = props => {
  const classes = useStyles();
  const theme = useTheme();
  const Styled = createStyled({
    badge: {
      backgroundColor: getColor(props.color, theme as Theme, props.colorBrightness),
    },
  });

  return (
    <Styled>
      {styledProps => (
        <BadgeBase
          classes={{
            badge: classnames(classes.badge, styledProps.classes.badge),
          }}
          badgeContent={props.badgeContent}
        >
          {props.children}
        </BadgeBase>
      )}
    </Styled>
  );
};

export default Badge;
