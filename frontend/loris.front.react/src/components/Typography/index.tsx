import React from 'react';
import { Theme, Typography as TypographyBase } from '@material-ui/core';
import { useTheme } from '@material-ui/styles';

import { getColor } from '../../helpers/colorHelper';
import { getFontSize, getFontWeight } from '../../helpers/fontHelper';

interface IProps {
  weight?: string;
  size?: string;
  colorBrightness?: string;
  color?: string;
  variant?: string;
  className?: string;
  component?: any;
  gutterBottom?: any;
  href?: any;
  onClick?: any;
}

const Typography: React.FC<IProps> = props => {
  const theme = useTheme();

  return (
    <TypographyBase
      className={props.className}
      component={props.component}
      gutterBottom={props.gutterBottom}
      href={props.href}
      onClick={props.onClick}
      style={{
        color: getColor(props.color, theme as Theme, props.colorBrightness),
        fontWeight: getFontWeight(props.weight),
        fontSize: getFontSize(props.size, props.variant, theme as Theme),
      }}
    >
      {props.children}
    </TypographyBase>
  );
};

export default Typography;
