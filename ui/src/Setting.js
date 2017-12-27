import React, { Component } from 'react';
import styled from 'styled-components';

const Conatiner = styled.div`
  height: 30%;
  width: 100%;
  position: fixed;
  z-index: 1;
  top: 70vh;
  left: 0;
  overflow-x: hidden;
  border: 1px solid green;
`;
export default class Setting extends Component {
  render() {
    return (
      <Conatiner
        id="setting"
      />
    );
  }
}
