const tokenEndpointUri = "http://localhost:5001/connect/token";
const tokenEndpointOptions = {
    method: "POST",
    headers: {
        "Content-Type": "application/x-www-form-urlencoded",
    },
    body:
        "grant_type=client_credentials" +
        "&client_id=tests" +
        "&client_secret=tests" +
        "&scope=api",
}


const getAccessToken = async (): Promise<string> => {
    const response = await fetch(tokenEndpointUri, tokenEndpointOptions);
    if (response.ok) {
        const data = await response.json();
        return data.access_token;
    } else {
        throw new Error("Error when receiving token.");
    }
}


const getApiUri = (relativeUri: string): string => `http://localhost:5003/api/${relativeUri}`;

const apiGet = async (uri: string, query?: Record<string, string>): Promise<Response> => {
    const accessToken = await getAccessToken();
    const queryParts = Object.keys(query ?? [])
        .map((key, i, query) =>
            encodeURIComponent(key)
            + "="
            + encodeURIComponent(query[i]));
    const queryString = queryParts.length === 0
        ? ""
        : "?" + queryParts.join("&");
    return await fetch(getApiUri(uri + queryString), {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${accessToken}`,
        },
    });
}

const apiPost = async (uri: string, body?: Record<string, unknown>): Promise<Response> => {
    const accessToken = await getAccessToken();
    return await fetch(getApiUri(uri), {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${accessToken}`,
        },
        body: JSON.stringify(body),
    });
}

const apiDelete = async (uri: string, query?: Record<string, string>): Promise<Response> => {
    const accessToken = await getAccessToken();
    const queryParts = Object.keys(query ?? [])
        .map((key, i, query) =>
            encodeURIComponent(key)
            + "="
            + encodeURIComponent(query[i]));
    const queryString = queryParts.length === 0
        ? ""
        : "?" + queryParts.join("&");
    return await fetch(getApiUri(uri + queryString), {
        method: "DELETE",
        headers: {
            "Authorization": `Bearer ${accessToken}`,
        },
    });
}


const controllerTest = (uri: string, idProperty: string, newEntity: Record<string, unknown>): void => describe(
    `Test ${uri}`,
    () => {
        test(`Get all from ${uri}`, async () => {
            const response = await apiGet(uri);
            expect(response.status).toBe(200);
            expect(Array.isArray(await response.json())).toBe(true);
        });


        test(`Get by id from ${uri}`, async () => {
            const responseAll = await apiGet(uri);
            const all = await responseAll.json();
            const id = all[0][idProperty];
            const response = await apiGet(`${uri}/${id}`);
            expect(response.status).toBe(200);
            expect(await response.json()).toHaveProperty(idProperty, id);
        });


        let createdEntityId: unknown;
        test(`Create from ${uri}`, async () => {
            const response = await apiPost(uri, newEntity);
            expect(response.status).toBe(201);
            const json = await response.json();
            expect(json).toMatchObject(newEntity);
            createdEntityId = json[idProperty];
        });


        test(`Delete from ${uri}`, async () => {
            const response = await apiDelete(`${uri}/${createdEntityId}`);
            expect(response.status).toBe(204);
            const verifyResponse = await apiGet(`${uri}/${createdEntityId}`);
            expect(verifyResponse.status).toBe(404);
        });
    }
);


export { controllerTest };

