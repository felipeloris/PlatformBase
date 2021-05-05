import { default as Palette } from '../palette';
import { default as Typography } from '../typography';

const MuiTableCell = (): any => {
  const palette = Palette();
  const typography = Typography();

  return {
    root: {
      ...typography.body1,
      borderBottom: `1px solid ${palette.divider}`,
    },
  };
};

export default MuiTableCell;
