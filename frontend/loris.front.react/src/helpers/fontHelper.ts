import { Theme } from '@material-ui/core';

const getFontWeight = (style: string | undefined = 'light'): number => {
  switch (style) {
    case 'light':
      return 300;
    case 'medium':
      return 500;
    case 'bold':
      return 600;
    default:
      return 400;
  }
};

const getFontSize = (size: string | undefined = 'md', variant = '', theme: Theme): string => {
  let multiplier: any;

  switch (size) {
    case 'sm':
      multiplier = 0.8;
      break;
    case 'md':
      multiplier = 1.5;
      break;
    case 'xl':
      multiplier = 2;
      break;
    case 'xxl':
      multiplier = 3;
      break;
    default:
      multiplier = 1;
      break;
  }

  const defaultSize =
    variant && theme.typography[variant]
      ? theme.typography[variant].fontSize
      : theme.typography.fontSize + 'px';

  return `calc(${defaultSize} * ${multiplier})`;
};

export { getFontWeight, getFontSize };
