import http from 'k6/http';
import { check, group, sleep } from 'k6';

export const options = {
    stages: [
        { target: 20, duration: '1m' },
        { target: 100, duration: '2m' },
        { target: 100, duration: '2m' },
        { target: 200, duration: '2m' },
        { target: 200, duration: '2m' },
        { target: 400, duration: '2m' },
        { target: 400, duration: '2m' },
        { target: 0, duration: '5m' }
    ]
};

export default function() {

    const res = http.get('https://localhost:44386/api/v1/Resources/All');

    sleep(1);
    const checkRes = check(res, {
        'Success': (r) => r.status === 200
    })

};