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
  const res = http.get('https://test.k6.io/my_messages.php');
  check(res, {
    'status is 200': (r) => r.status === 200,
  });
}