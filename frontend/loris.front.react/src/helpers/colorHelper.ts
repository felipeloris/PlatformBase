import { Theme } from '@material-ui/core';

export function getColor(color: string | undefined, theme: Theme, brigtness = 'main'): any {
  if (color && theme.palette[color] && theme.palette[color][brigtness]) {
    return theme.palette[color][brigtness];
  }
}
