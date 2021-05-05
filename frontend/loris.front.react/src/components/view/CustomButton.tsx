/* eslint-disable react/react-in-jsx-scope */
import Button from '@material-ui/core/Button';
import styled from 'styled-components';

const CustomButton = styled(({ ...other }) => <Button {...other} />)`
  border: 0;
  height: 22px;
  padding: 0 20px;

  & .MuiButton-label {
    font-size: 0.8rem;
  }
`;

export default CustomButton;
