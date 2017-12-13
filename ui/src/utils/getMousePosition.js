let x = 0;
let y = 0;

document.addEventListener('mousemove', (e) => {
  x = e.pageX;
  y = e.pageY;
});

export default function getMousePosition() {
  return {
    x,
    y,
  };
}
