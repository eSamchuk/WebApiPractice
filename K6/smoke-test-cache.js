import http from 'k6/http';
import { check, group, sleep } from 'k6';

export const options = {
    stages: [
        { target: 2, duration: '1m' },
    ]
};

export default function() {

    const res = http.get('https://localhost:44386/api/v1/Resources/All/WithCache');

    sleep(1);
    const checkRes = check(res, {
        'Success': (r) => r.status === 200
    })

};