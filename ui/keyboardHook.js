const { globalShortcut, ipcMain } = require('electron');

ipcMain.on('register', (event, accelerator) => {
  if (globalShortcut.isRegistered(accelerator)) {
    return;
  }
  const ret = globalShortcut.register(accelerator, () => {
    event.sender.send('onkeyhook', accelerator);
  });
  if (!ret) {
    console.log(`registration failed, ${accelerator}`);
  }
});

ipcMain.on('unregister', (event, accelerator) => {
  if (!globalShortcut.isRegistered(accelerator)) {
    return;
  }
  const ret = globalShortcut.unregister(accelerator);
  if (!ret) {
    console.log(`registration failed, ${accelerator}`);
  }
});
