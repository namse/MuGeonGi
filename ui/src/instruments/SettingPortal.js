import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { onSingleBoxClicked } from './SingleBox';

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
    return ReactDOM.createPortal(
      this.props.children,
      settingDom,
    );
  }
}
