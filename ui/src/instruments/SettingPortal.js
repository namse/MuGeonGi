import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import styled from 'styled-components';
import { onSingleBoxClicked } from './SingleBox';
import { deleteInstrument } from '../App';

const DeleteButton = styled.button`
  float: right;
`;

export default class SettingPortal extends Component {
  constructor(props) {
    super(props);
    this.state = {
      show: false,
    };
    onSingleBoxClicked((singleBox) => {
      this.setState({
        show: (singleBox.props.uuid === props.uuid),
      });
    });
  }
  render() {
    const settingDom = document.getElementById('setting');
    if (!settingDom || !this.state.show) {
      return false;
    }
    const settingWindow = (
      <div>
        <DeleteButton
          onClick={() => deleteInstrument(this.props.uuid)}
        >Delete
        </DeleteButton>
        {/* Note: children's state must be deleted
          when this becomes unmounted - when open other setting window! */}
        {this.props.children}
      </div>);
    return ReactDOM.createPortal(
      settingWindow,
      settingDom,
    );
  }
}
