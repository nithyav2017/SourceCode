async function testConcurrency(count) {
  let start = performance.now();
  let promises = [];
  for (let i = 0; i < count; i++) {
    promises.push(fetch("https://localhost:7241/api/Entity/Data"));
  }
  await Promise.all(promises);
  let end = performance.now();
  console.log(`Sent ${count} concurrent requests in ${(end - start).toFixed(2)} ms`);
}

 
testConcurrency(50);
