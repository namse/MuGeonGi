import hookKeyboard, {
  convertToAccelerator,
  pauseKeyboardHooking,
  resumeKeyboardHooking,
} from '../utils/hookKeyboard';

const SHIFT = 16;
const CTRL = 17;
const ALT = 18;
const combinationKeys = [SHIFT, CTRL, ALT];

export default class KeyBindingHelper {
  constructor(
    onKeyDown = () => { },
    onAcceleratorChanged = () => { },
  ) {
    this.onKeyDown = onKeyDown;
    this.onAcceleratorChanged = onAcceleratorChanged;
    this.keyHookSymbol = Symbol('KeyBindingHelper');
  }
  bindKey() {
    const handler = (event) => {
      event.preventDefault();
      if (combinationKeys.includes(event.which)) {
        return;
      }
      hookKeyboard(this.keyHookSymbol, event, this.onKeyDown);

      const accelerator = convertToAccelerator(event);
      this.onAcceleratorChanged(accelerator);

      document.removeEventListener('keydown', handler);
      resumeKeyboardHooking();
    };
    pauseKeyboardHooking();
    document.addEventListener('keydown', handler);
  }
}
