const { ipcRenderer } = window.require('electron');

const hookInfoList = []; // { symbol, accelerator, handler }

export function pauseKeyboardHooking() {
  ipcRenderer.send('unregisterAll');
}

export function resumeKeyboardHooking() {
  hookInfoList.forEach((info) => {
    ipcRenderer.send('register', info.accelerator);
  });
}

export function convertToAccelerator(keyInfo) {
  const {
    shiftKey,
    ctrlKey,
    altKey,
    key,
  } = keyInfo;
  const accelerator = `${shiftKey ? 'Shift+' : ''}${ctrlKey ? 'Ctrl+' : ''}${altKey ? 'Alt+' : ''}${key}`;
  return accelerator;
}

ipcRenderer.on('onkeyhook', (event, accelerator) => {
  hookInfoList.forEach((info) => {
    if (info.accelerator === accelerator) {
      info.handler();
    }
  });
});

export default function hookKeyboard(symbol, keyInfo, handler) {
  const accelerator = convertToAccelerator(keyInfo);
  let hookInfo = hookInfoList.find(info => info.symbol === symbol);

  if (!hookInfo) {
    hookInfo = {
      symbol,
      accelerator,
      handler,
    };
    hookInfoList.push(hookInfo);
  } else {
    const previousAccelerator = hookInfo.accelerator;
    hookInfo.accelerator = accelerator;
    hookInfo.handler = handler;
    if (previousAccelerator !== accelerator) {
      const previousHookLength = hookInfoList.filter(info =>
        info.accelerator === previousAccelerator).length;
      if (previousHookLength === 0) {
        ipcRenderer.send('unregister', previousAccelerator);
      }
    }
  }
  const hookLength = hookInfoList.filter(info =>
    info.accelerator === accelerator).length;
  if (hookLength === 1) {
    ipcRenderer.send('register', accelerator);
  }
}

