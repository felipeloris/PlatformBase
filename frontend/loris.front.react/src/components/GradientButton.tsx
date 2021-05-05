/* eslint-disable react/react-in-jsx-scope */
import styled from 'styled-components';
import Button from '@material-ui/core/Button';

const GradientButton = styled(({ fontColor, ...other }) => <Button {...other} />)`
  background: linear-gradient(45deg, #6b99fe 30%, #1d53c7 90%);
  border: 0;
  color: white;
  height: 22px;
  padding: 0 20px;
  box-shadow: 0 3px 5px 2px rgba(123, 160, 230, 0.3);

  & .MuiButton-label {
    color: ${props => props.fontColor};
    font-size: 0.8rem;
  }
`;

export default GradientButton;
