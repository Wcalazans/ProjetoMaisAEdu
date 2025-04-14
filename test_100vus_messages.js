import { check } from 'k6';
import http from 'k6/http';

// Opções de teste
export const options = {
  vus: 100,
  duration: '1m',
  thresholds: {
    http_req_failed: ['rate<0.1'], // Menos de 0.1% de falhas
  },
};

// Função principal do teste
export default function () {
  const res = http.get('https://test.k6.io/my_messages.php');
  check(res, {
    'status is 200': (r) => r.status === 200,
  });
}

// Função para gerar o relatório em HTML
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

export function handleSummary(data) {
  return {
    "resultado_teste.html": htmlReport(data), // Define o nome do arquivo HTML gerado
  };
}
