import http from 'k6/http';
import { check, group, sleep } from 'k6';

export const options = {
    stages: [
        { target: 100, duration: '3m' },
        { target: 100, duration: '5m' },
        { target: 0, duration: '3m' }
    ]
};

export default function() {

    const res = http.get('https://localhost:44386/api/v1/Resources/All');

    sleep(1);
    const checkRes = check(res, {
        'Success': (r) => r.status === 200
    })

};