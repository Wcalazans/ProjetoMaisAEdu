import { check } from 'k6';
import http from 'k6/http';

export const options = {
  vus: 500,
  duration: '1m',
  thresholds: {
    http_req_failed: ['rate<0.1'],
  },
};

export default function () {
  const res1 = http.post(
    'https://test.k6.io/my_messages.php',
    'username=test&password=test',
    {
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
    }
  );

  check(res1, {
    'status is 200': (r) => r.status === 200,
  });

  const res2 = http.get('https://test.k6.io/flip_coin.php');

  check(res2, {
    'status is 200': (r) => r.status === 200,
  });
}
