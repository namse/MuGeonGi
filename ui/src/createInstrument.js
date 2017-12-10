export default instrument =>
  fetch(`http://localhost:8080/${instrument}`, {
    method: 'POST',
  })
    .then(res => res.text());
